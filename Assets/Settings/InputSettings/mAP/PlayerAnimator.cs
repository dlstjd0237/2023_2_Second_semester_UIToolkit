using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset[] _spriteAssets;
    [SerializeField] private SpriteRenderer _shockwaveSprite;

    private SpriteLibrary _spriteLibrary;
    private Animator _animator;

    private int _hashIsMove = Animator.StringToHash("is_move");
    private int _currentSpriteIndex = 0;

    private readonly int _hashWaveDistance = Shader.PropertyToID("_WaveDistance");
    private Material _shockwaveMat;

    private void Awake()
    {
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _animator = GetComponent<Animator>();

        //_shockwaveSprite = GetComponent<>
    }

    public void SetMovement(bool value)
    {
        _animator.SetBool(_hashIsMove, value);
    }

    public void SetNextSprite()
    {
        _currentSpriteIndex = (_currentSpriteIndex + 1) % _spriteAssets.Length;
        _spriteLibrary.spriteLibraryAsset = _spriteAssets[_currentSpriteIndex];
    }

    private Tween _shockTween;
    private void ShockwaveEffect()
    {
        if (_shockTween != null && _shockTween.IsActive == true)

        _shockwaveSprite.gameObject.SetActive(true);
        _shockwaveMat.SetFloat(_hashWaveDistance, -0.1f);

        _shockTween = DOTween.TO(() => _shockwaveMat.GetFloat(_hashWaveDistance), value => _shockwaveMat.SetFloat(_hashWaveDistance, value), 1f, 0.6f).OnComplete(() => _shockwaveSprite.gameObject.SetActive(false);

    }
}
